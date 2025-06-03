import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { IncidentModel } from '../IncidentModels/incident-model';
import { IncidentService } from '../IncidentServices/incident.service';

@Component({
  selector: 'app-view-incident',
  standalone: false,
  templateUrl: './view-incident.component.html',
  styleUrl: './view-incident.component.css'
})
export class ViewIncidentComponent implements OnInit{

  public incidentList: IncidentModel[] = [];
  constructor(private incidentService: IncidentService, private cd: ChangeDetectorRef) { }

  public isModalVisible = false;
  public componentName: string = '';
  public incidentId: string = '';

  ngOnInit() {
    this.loadView()
  }

  showModal(component: string, id:string) {
    this.isModalVisible = true;
    this.componentName = component;
    this.incidentId = id;
  }

  hideModal(result: any) {
    this.isModalVisible = false;
    this.cd.detectChanges();
  }

  private loadView() {
    var incidents = this.incidentService.getAllIncidents().subscribe(response => {
      this.incidentList = response.result;
      console.log(this.incidentList);
    });
  }
}
