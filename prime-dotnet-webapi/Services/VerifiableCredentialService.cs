using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.Models;
using Prime.Services.Clients;
using Newtonsoft.Json.Linq;

namespace Prime.Services
{
    public enum WebhookTopic
    {
        Connections,
        IssueCredential
    }

    public enum CredentialExchangeState
    {
        RequestReceived,
        Issued
    }

    public class VerifiableCredentialService : BaseService, IVerifiableCredentialService
    {
        private readonly IVerifiableCredentialClient _verifiableCredentialClient;
        public VerifiableCredentialService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IVerifiableCredentialClient verifiableCredentialClient)
            : base(context, httpContext)
        {
            _verifiableCredentialClient = verifiableCredentialClient;
        }

        public async Task<JObject> CreateConnection()
        {
            var invitationResponse = await _verifiableCredentialClient.CreateInvitation();
            var invitation = invitationResponse.GetValue("invitation");

            if (invitation == null)
            {
                return invitationResponse;
            }

            var receiveResponse = await _verifiableCredentialClient.ReceiveInvitation(invitation.ToString());
            var connection_id = receiveResponse.GetValue("connection_id");

            if (connection_id == null)
            {
                return receiveResponse;
            }

            var acceptResponse = await _verifiableCredentialClient.AcceptInvitation(connection_id.ToString());
            return acceptResponse;
        }


        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L30
        public async Task<bool> create(Object data, WebhookTopic topic)
        {
            switch (topic)
            {
                case WebhookTopic.Connections:
                    return await handleConnection(data);
                case WebhookTopic.IssueCredential:
                    return await handleIssueCredential(data);
                default:
                    // TODO log a message $"Webhook {topic} is not supported";
                    return false;
            };
        }

        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L44
        private async Task<bool> handleConnection(Object data)
        {
            // TODO implement connection webhook logic
            return await Task.FromResult(true);
        }

        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L48
        private async Task<bool> handleIssueCredential(Object data)
        {
            var state = CredentialExchangeState.RequestReceived; // TODO data.state;
            // TODO switch statement based on the credential exchange state
            switch (state)
            {
                case CredentialExchangeState.RequestReceived:
                    // TODO call aries agent to create credential
                    return await Task.FromResult(true);
                case CredentialExchangeState.Issued:
                    // TODO updateInviteRecord(...);
                    return await Task.FromResult(true);
                default:
                    // TODO log a message $"Received unexpected state {state} for CredentialExchangeState ${state.id}"
                    return await Task.FromResult(false);
            }
        }
    }
}
