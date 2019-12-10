import { Address } from '../../modules/enrolment/shared/models/address.model';
import { EnrolleeProfile } from './enrollee-profile.model';

export interface Enrollee extends EnrolleeProfile {
  id?: number;
  userId: string;
  physicalAddress: Address;
  mailingAddress?: Address;
  contactEmail: string;
  contactPhone: string;
  voicePhone: string;
  voiceExtension: string;
}