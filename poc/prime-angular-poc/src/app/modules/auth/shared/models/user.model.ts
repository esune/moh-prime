export interface User {
  userId: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  physicalAddress: {
    countryCode: string;
    provinceCode: string;
    street: string;
    city: string;
    postal: string;
  };
  contactEmail: string;
}
