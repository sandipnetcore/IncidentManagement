import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryModel } from '../../category/category-model';
import { EndPointAddress } from '../../Common/end-point-address';
import { IncidentCommentModel } from '../IncidentModels/incident-comment-model';
import { IncidentDetailModel } from '../IncidentModels/incident-detail-model';
import { IncidentModel } from '../IncidentModels/incident-model';
import { IncidentStatusModel } from '../IncidentModels/incident-status-model';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {
  constructor(private httpClient: HttpClient) { }

  public getAllStatusTypes(): Observable<any> {
    return this.httpClient.get<IncidentStatusModel[]>(EndPointAddress.Status_GetAllStatusTypes);
  }

  public getIncidentDetails(incidentId:string): Observable<any> {
    return this.httpClient.get<IncidentDetailModel>(EndPointAddress.Incident_GetDetailsById + incidentId);
  }

  public getAllIncidents(): Observable<any> {
    return this.httpClient.get<IncidentModel[]>(EndPointAddress.Incident_GetAllIncident);
  }

  public createIncident(model:IncidentModel): Observable<any> {
    return this.httpClient.post<any>(EndPointAddress.Incident_CreateIncident, model);
  }

  public AddComents(model: IncidentCommentModel): Observable<any> {
    return this.httpClient.post<any>(EndPointAddress.Incident_AddComents + model.incidentId, model);
  }

}
