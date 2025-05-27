import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { PopupModelComponent } from './Shared/popup-model/popup-model.component';
import { authTokenInterceptor } from './Shared/Interceptor/auth-token.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { LeftPanelComponent } from './left-panel/left-panel.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { CreateIncidentComponent } from './incident/create-incident/create-incident.component';
import { DeleteIncidentComponent } from './incident/delete-incident/delete-incident.component';
import { EditIncidentComponent } from './incident/edit-incident/edit-incident.component';
import { ViewIncidentComponent } from './incident/view-incident/view-incident.component';
import { ViewCategoryComponent } from './category/view-category/view-category.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { DeleteCategoryComponent } from './category/delete-category/delete-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    PopupModelComponent,
    LeftPanelComponent,
    HomeComponent,
    FooterComponent,
    DashboardComponent,
    CreateIncidentComponent,
    DeleteIncidentComponent,
    EditIncidentComponent,
    ViewIncidentComponent,
    ViewCategoryComponent,
    CreateCategoryComponent,
    DeleteCategoryComponent,
    EditCategoryComponent,
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    RouterModule,
    BrowserAnimationsModule, NgxSpinnerModule,
    NgxSpinnerModule.forRoot({ type: 'ball-clip-rotate-multiple' })
  ],
  providers: [
    provideHttpClient(withInterceptors([authTokenInterceptor])),
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
