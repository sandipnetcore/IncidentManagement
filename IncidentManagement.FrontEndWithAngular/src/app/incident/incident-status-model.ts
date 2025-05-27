export class IncidentStatusModel {
  public statusId: number = 0;
  public statusName: string = '';

  constructor(statusId: number, statusName: string) {
    this.statusId = statusId;
    this.statusName = statusName;
  }
}
