import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IncidentCommentModel } from '../IncidentModels/incident-comment-model';
import { IncidentDetailModel } from '../IncidentModels/incident-detail-model';
import { IncidentService } from '../IncidentServices/incident.service';

@Component({
  selector: 'app-edit-incident',
  standalone: false,
  templateUrl: './edit-incident.component.html',
  styleUrl: './edit-incident.component.css'
})
export class EditIncidentComponent implements OnInit {

  @Input() incidentId: string = '';
  @Output() onAddComments = new EventEmitter<any>();
  constructor(private incidentService: IncidentService, private router: Router) { }

  public incientsDetails: IncidentDetailModel = new IncidentDetailModel();

  ngOnInit(): void {
    var indient = this.incidentService.getIncidentDetails(this.incidentId).subscribe(response => {
      this.incientsDetails = response.result;
      console.log(this.incientsDetails);
    });
  }

  public commentsForm = new FormGroup({
    comments: new FormControl('', [Validators.required]),
    assignedto: new FormControl('', [
    ]),
  });

  public async saveComments() {
    if (!this.commentsForm.valid) {
      return;
    }
    let commentsModel = new IncidentCommentModel();
    commentsModel.commentText = this.commentsForm.value.comments as string;
    commentsModel.assignedToUser = this.commentsForm.value.assignedto as string;
    commentsModel.incidentId = this.incidentId;
    var token = '';
    var result = await this.incidentService.AddComents(commentsModel).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error.message);
      }
    );

    this.onAddComments.emit();

  }
}
