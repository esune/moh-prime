import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, EMPTY, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-site-overview',
  templateUrl: './site-overview.component.html',
  styleUrls: ['./site-overview.component.scss']
})
export class SiteOverviewComponent implements OnInit, IPage {
  public busy: Subscription;
  public title: string;
  public routeUtils: RouteUtils;
  public site: Site;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private organizationResource: OrganizationResource,
    private siteFormStateService: SiteFormStateService,
    private dialog: MatDialog
  ) {
    this.title = 'Site Information Review';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    const organizationId = this.route.snapshot.params.oid;
    // TODO shouldn't come from service when spoking to save updates
    const payload = this.siteService.site;
    const data: DialogOptions = {
      title: 'Save Site',
      message: 'When your site is saved it will be submitted for review. Are you ready to save your site?',
      actionText: 'Save Site'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.siteResource.submitSite(payload)
            : EMPTY
        ),
        exhaustMap(() => this.organizationResource.getOrganizationById(organizationId)),
        map((organization: Organization) => !!organization.acceptedAgreementDate)
      )
      .subscribe((hasSignedOrgAgreement: boolean) => this.nextRoute(organizationId, hasSignedOrgAgreement));
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.TECHNICAL_SUPPORT);
  }

  public nextRoute(organizationId: number, hasSignedOrgAgreement: boolean) {
    console.log(hasSignedOrgAgreement, organizationId);

    if (!hasSignedOrgAgreement) {
      this.routeUtils.routeTo([SiteRoutes.routePath(SiteRoutes.ORGANIZATIONS), organizationId, SiteRoutes.ORGANIZATION_AGREEMENT]);
    } else {
      this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS], {
        queryParams: { submitted: true }
      });
    }
  }

  public onRoute(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit() {
    this.site = this.siteService.site;

  }
}
