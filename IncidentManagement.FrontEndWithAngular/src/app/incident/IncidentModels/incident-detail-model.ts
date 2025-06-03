import { IncidentCommentModel } from "./incident-comment-model";

export class IncidentDetailModel {
  public incidentId: string = '';
  public incidentTitle: string = '';
  public incidentDescription: string = '';
  public category: string = '';
  public createdBy: string = '';
  public createdOn: string = '';
  public assignedToUser: string = '';
  public incidentStatus: string = '';
  public incidentComments: IncidentCommentModel[] = [];
  constructor() { }
}
