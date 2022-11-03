import { Injectable } from '@angular/core';
import { IPaginatedListOfShopModel, IShopModel, ShopListClient } from '@shared-module';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  constructor(private shopListClient: ShopListClient) {}

  shops: IShopModel[] = [];

  getShops() {
    this.shopListClient.getShopss(undefined, undefined).subscribe(data => {
      this.shops = data.items ?? [];
    });
  }
}
