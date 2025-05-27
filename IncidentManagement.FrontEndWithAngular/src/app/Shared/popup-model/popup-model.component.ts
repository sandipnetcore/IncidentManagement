import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LoginConstants } from '../../Common/constants';

@Component({
  selector: 'app-popup-model',
  templateUrl: './popup-model.component.html',
  styleUrl: './popup-model.component.css',
  standalone: false
})
export class PopupModelComponent {
@Output() close = new EventEmitter<any>();

  @Input() componentName: string = '';
  @Input() incidentId: string = '';

  public result: any;

  public loginClick(data:any) {
    this.result = data;
    if (localStorage.getItem(LoginConstants.jwtTokenKey)) {
      this.closeModal();
    }
  }

  public afterAddCommentsEmitter(data: any) {
    this.close.emit();
  }

  closeModal(): void {
    this.close.emit(this.result);
  }
}
