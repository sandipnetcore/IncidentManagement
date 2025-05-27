export class EndPointAddress {
  public static readonly API_Address = "https://localhost:7006/";
  public static readonly Login_API: string = this.API_Address + "login";

  public static readonly GetAllCategories: string = this.API_Address + "Category/GetAllCategories";

  public static readonly GetAllIncident: string = this.API_Address + "Incident/GetAllIncident";
  public static readonly GetAllStatusTypes: string = this.API_Address + "Incident/GetAllStatusTypes";
  public static readonly GetDetailsById: string = this.API_Address + "Incident/GetDetailsById/";
  public static readonly CreateIncident: string = this.API_Address + "Incident/CreateIncident";
  public static readonly AddComents: string = this.API_Address + "Incident/AddComments/";
}
