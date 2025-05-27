import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { CreateIncidentComponent } from './incident/create-incident/create-incident.component';
import { ViewIncidentComponent } from './incident/view-incident/view-incident.component';
import { DeleteIncidentComponent } from './incident/delete-incident/delete-incident.component';
import { EditIncidentComponent } from './incident/edit-incident/edit-incident.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { ViewCategoryComponent } from './category/view-category/view-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { DeleteCategoryComponent } from './category/delete-category/delete-category.component';

const routes: Routes = [

  { path: 'home', component: HomeComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'incident/create-incident', component: CreateIncidentComponent },
  { path: 'incident/view-incident', component: ViewIncidentComponent },
  { path: 'incident/delete-incident', component: DeleteIncidentComponent },
  { path: 'incident/edit-incident', component: EditIncidentComponent },
  { path: 'category/view-category', component: ViewCategoryComponent },
  { path: 'category/create-category', component: CreateCategoryComponent },
  { path: 'category/edit-category', component: EditCategoryComponent },
  { path: 'category/delete-category', component: DeleteCategoryComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
