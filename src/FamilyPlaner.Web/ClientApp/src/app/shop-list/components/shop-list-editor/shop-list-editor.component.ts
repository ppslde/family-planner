import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-shop-list-editor',
  templateUrl: './shop-list-editor.component.html',
  styleUrls: ['./shop-list-editor.component.scss'],
})
export class ShopListEditorComponent implements OnInit {
  constructor(private srv: ShopService) {}

  async ngOnInit() {
    let results = await Promise.allSettled([
      this.srv.getShops(),
      this.srv.getCategories(),
    ]);
    let x = results[0];
  }
}
