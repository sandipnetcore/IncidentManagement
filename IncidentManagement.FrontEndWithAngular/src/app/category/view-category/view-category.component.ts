import { Component, OnInit } from '@angular/core';
import { CategoryModel } from '../category-model';
import { CategoryService } from '../CategoryServices/category.service';

@Component({
  selector: 'app-view-category',
  standalone: false,
  templateUrl: './view-category.component.html',
  styleUrl: './view-category.component.css'
})
export class ViewCategoryComponent implements OnInit {
  constructor(private categoryService: CategoryService) { }

  public categoryList: CategoryModel[] = [];


  ngOnInit(): void {
    var result = this.categoryService.getAllCategoriesTypes().subscribe(response => {
      this.categoryList = response.result;
      console.log(this.categoryList);
    });
  }
}
