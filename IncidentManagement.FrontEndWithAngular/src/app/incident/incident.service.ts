import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EndPointAddress } from '../Common/end-point-address';
import { IncidentModel } from './incident-model';
import { CategoryModel } from './category-model';
import { IncidentStatusModel } from './incident-status-model';
import { IncidentDetailModel } from './incident-detail-model';
import { IncidentCommentModel } from './incident-comment-model';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {
  constructor(private httpClient: HttpClient) { }

  //public loginStatus: string = '';

  public getAllCategoriesTypes(): Observable<any> {
    return this.httpClient.get<CategoryModel[]>(EndPointAddress.GetAllCategories);
  }

  public getAllStatusTypes(): Observable<any> {
    return this.httpClient.get<IncidentStatusModel[]>(EndPointAddress.GetAllStatusTypes);
  }

  public getIncidentDetails(incidentId:string): Observable<any> {
    return this.httpClient.get<IncidentDetailModel>(EndPointAddress.GetDetailsById + incidentId);
  }

  public getAllIncidents(): Observable<any> {
    return this.httpClient.get<IncidentModel[]>(EndPointAddress.GetAllIncident);
  }

  public createIncident(model:IncidentModel): Observable<any> {
    return this.httpClient.post<any>(EndPointAddress.CreateIncident, model);
  }

  public AddComents(model: IncidentCommentModel): Observable<any> {
    return this.httpClient.post<any>(EndPointAddress.AddComents + model.incidentId, model);
  }

}
