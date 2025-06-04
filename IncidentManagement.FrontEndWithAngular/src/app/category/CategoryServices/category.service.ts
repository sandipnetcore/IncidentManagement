import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EndPointAddress } from '../../Common/end-point-address';
import { CategoryModel } from '../category-model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient: HttpClient) { }

  public getAllCategoriesTypes(): Observable<any> {
    return this.httpClient.get<CategoryModel[]>(EndPointAddress.Category_GetAllCategories);
  }

  public getCategoryById(categoryId: number): Observable<any> {
    return this.httpClient.get<CategoryModel>(`${EndPointAddress.Category_GetCategoryById}${categoryId}`);
  }

  public addCategoryType(model: CategoryModel): Observable<any> {
    return this.httpClient.post<any>(EndPointAddress.Category_AddCagtegory, model);
  }

  public modifyCategoryType(model: CategoryModel): Observable<any> {
    return this.httpClient.put<any>(EndPointAddress.Category_ModifyCategory, model);
  }

  public deleteCategoryType(categoryId: string): Observable<any> {
    alert(`${EndPointAddress.Category_DeleteCategory}${categoryId}`);
    return this.httpClient.delete<any>(`${EndPointAddress.Category_DeleteCategory}${categoryId}`);
  }

}
