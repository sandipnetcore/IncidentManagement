import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteIncidentComponent } from './delete-incident.component';

describe('DeleteIncidentComponent', () => {
  let component: DeleteIncidentComponent;
  let fixture: ComponentFixture<DeleteIncidentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteIncidentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteIncidentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
