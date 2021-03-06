import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

export interface AccessTerm {
  id: number;
  enrolleeId: number;
  termsOfAccess: string;
  effectiveDate: string;
  createdDate: string;
  acceptedDate?: string;
  expiryDate?: string;
}

export interface Clause {
  id: number;
  clause: string;
  effectiveDate: string;
}

export interface UserClause extends Clause {
  enrolleeClassification: EnrolleeClassification;
}
