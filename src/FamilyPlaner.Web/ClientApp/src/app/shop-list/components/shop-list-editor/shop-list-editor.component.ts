import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-shop-list-editor',
  templateUrl: './shop-list-editor.component.html',
  styleUrls: ['./shop-list-editor.component.scss'],
})
export class ShopListEditorComponent implements OnInit {
  constructor(private srv: ShopService) {}

  ngOnInit(): void {
    this.srv.getShops();
  }
}
