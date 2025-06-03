import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryModel } from '../category-model';
import { CategoryService } from '../CategoryServices/category.service';

@Component({
  selector: 'app-create-category',
  standalone: false,
  templateUrl: './create-category.component.html',
  styleUrl: './create-category.component.css'
})
export class CreateCategoryComponent {

  constructor(private categoryService: CategoryService) { }

  public categoryForm = new FormGroup({
    categoryName: new FormControl('', [Validators.required]),
    categoryDescription: new FormControl('', [
      Validators.required,
      Validators.minLength(12),
      Validators.maxLength(20),
    ]),
  });

  public createCategory() {
    if (this.categoryForm.valid) {
      var categoryModel: CategoryModel = {
        categoryId: '',
        categoryName: this.categoryForm.value.categoryName as string,
        categoryDescription: this.categoryForm.value.categoryDescription as string,
        createdBy: '',
        createdOn: '',
        modifiedBy: '',
        modifiedOn: '',
      };

      this.categoryService.addCategoryType(categoryModel).subscribe(response => {
        if (response.success) {
          alert('Category created successfully!');
          this.categoryForm.reset();
        } else {
          alert('Failed to create category: ' + response.message);
        }
      });
    }
  }
}

