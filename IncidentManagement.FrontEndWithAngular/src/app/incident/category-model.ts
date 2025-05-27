export class CategoryModel {
  public categoryId: string = '';
  public categoryName: string = '';

  constructor(categoryId: string, categoryName: string) {
    this.categoryId = categoryId;
    this.categoryName = categoryName;
  }

}
