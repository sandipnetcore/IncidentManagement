import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryModel } from '../../category/category-model';
import { IncidentModel } from '../IncidentModels/incident-model';
import { IncidentStatusModel } from '../IncidentModels/incident-status-model';
import { IncidentService } from '../IncidentServices/incident.service';
import { CategoryService } from '../../category/CategoryServices/category.service';

@Component({
  selector: 'app-create-incident',
  standalone: false,
  templateUrl: './create-incident.component.html',
  styleUrl: './create-incident.component.css'
})
export class CreateIncidentComponent implements OnInit{

  public result: any;
  public categorylist: CategoryModel[] = [];
  public incidentStatusList: IncidentStatusModel[] = [];


  constructor(private incidentService: IncidentService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    var resultCategory = this.categoryService.getAllCategoriesTypes().subscribe(response => {
      this.categorylist = response.result as CategoryModel[];
    });

    var resultStatus = this.incidentService.getAllStatusTypes().subscribe(response => {
      this.incidentStatusList = response.result as IncidentStatusModel[];
    });
  }
  

  
  public incidentForm = new FormGroup({
    incidentTitle: new FormControl('', [Validators.required]),
    incidentDescription: new FormControl('', [
      Validators.required,
      Validators.maxLength(100),
    ]),
    incidentStatus: new FormControl('', [
      Validators.required,
      Validators.min(1),
      Validators.max(3),
      Validators.maxLength(1),
    ]),
    categoryId: new FormControl('', [
      Validators.required,
      Validators.min(1),
      Validators.max(3),
      Validators.maxLength(1),
    ]),
  });

  public onCategorySelected(value:string) {
    this.incidentForm.value.categoryId = value;
  }

  public createIncident() {

    var incidentModel: IncidentModel = {
      incidentId: '',
      incidentTitle: this.incidentForm.value.incidentTitle as string,
      incidentDescription: this.incidentForm.value.incidentDescription as string,
      incidentStatus: '',
      categoryId: this.incidentForm.value.categoryId as string,
      categoryName: '',
      createdOn: '',
      userName: '',
      assignedToUser : ''
    };

    this.incidentService.createIncident(incidentModel).subscribe(response => {
      console.log(response);
    });
  }
}
