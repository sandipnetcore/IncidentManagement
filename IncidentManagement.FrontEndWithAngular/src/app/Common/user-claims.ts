import { LoginConstants } from "./constants";

export class UserClaims {
  public UserName: string = '';
  public FirstName: string = '';
  public LastName: string = '';
  public roles: string[] = [];
  public expiry: string = '';

  constructor(token: any) {
    if (token) {
      this.UserName = token[LoginConstants.claimsUserName];
      this.FirstName = token[LoginConstants.claimsFirstName];
      this.LastName = token[LoginConstants.claimsLastName];
      this.roles = Array.isArray(token[LoginConstants.claimsRoles]) ? token[LoginConstants.claimsRoles] : [];
      this.expiry = token[LoginConstants.claimsExpiryTime];
    }
  }
}
