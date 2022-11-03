import { Injectable } from '@angular/core';
import {
  CategoryClient,
  IShopModel,
  ItemClient,
  PaginatedListOfCategoryModel,
  PaginatedListOfShopModel,
  ProductClient,
  ShopClient,
} from '@shared-module';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  constructor(
    private shops: ShopClient,
    private catefories: CategoryClient,
    private products: ProductClient,
    private items: ItemClient
  ) {}

  getCategories(): Promise<PaginatedListOfCategoryModel> {
    return new Promise<PaginatedListOfCategoryModel>(resolve => {
      this.catefories
        .getCategoryProducts(undefined, undefined)
        .subscribe(async v => {
          resolve(v);
        });
    });
  }

  getShops(): Promise<PaginatedListOfShopModel> {
    return new Promise<PaginatedListOfShopModel>(resolve => {
      this.shops.getShopss(undefined, undefined).subscribe(async v => {
        resolve(v);
      });
    });
  }
}
