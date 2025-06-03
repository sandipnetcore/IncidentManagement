export class EndPointAddress {

  //WEB URL
  public static readonly API_Address = "https://localhost:7006/";

  //LOGIN END POINT
  public static readonly Login_API: string = this.API_Address + "login";

  //CATEGORY END POINT
  public static readonly Category_GetAllCategories: string = `${this.API_Address}Category/GetAllCategories`;
  public static readonly Category_GetCategoryById: string = `${this.API_Address}Category/GetCategory/`;
  public static readonly Category_AddCagtegory: string = `${this.API_Address}Category/AddCategory`;
  public static readonly Category_ModifyCategory: string = `${this.API_Address}Category/ModifyCategory`;
  public static readonly Category_DeleteCategory: string = `${this.API_Address}Category/DeleteCategory/`;

  //INCIDENT END POINT
  public static readonly Incident_GetAllIncident: string = `${this.API_Address}Incident/GetAllIncident`;
  public static readonly Incident_GetDetailsById: string = `${this.API_Address}Incident/GetDetailsById/`;
  public static readonly Incident_CreateIncident: string = `${this.API_Address}Incident/CreateIncident`;
  public static readonly Incident_AddComents: string = `${this.API_Address}Incident/AddComments/`;

  //STATUS END POINT
  public static readonly Status_GetAllStatusTypes: string = `${this.API_Address}Status/GetAllStatusTypes`;

}
