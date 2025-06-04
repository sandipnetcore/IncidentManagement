import { Component, OnInit } from '@angular/core';
import { CategoryModel } from '../category-model';
import { CategoryService } from '../CategoryServices/category.service';

@Component({
  selector: 'app-delete-category',
  standalone: false,
  templateUrl: './delete-category.component.html',
  styleUrl: './delete-category.component.css'
})
export class DeleteCategoryComponent implements OnInit {
  constructor(private categoryService: CategoryService) { }

  public categoryList: CategoryModel[] = [];

  ngOnInit(): void {
    var result = this.categoryService.getAllCategoriesTypes().subscribe(response => {
      this.categoryList = response.result;
      console.log(this.categoryList);
    });
  }

  public deleteCategory(id: string) {
    alert(id);
    var result = this.categoryService.deleteCategoryType(id).subscribe(response => {
      console.log(response);
    });
  }
}
