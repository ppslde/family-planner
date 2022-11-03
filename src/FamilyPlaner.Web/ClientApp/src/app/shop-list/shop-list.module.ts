import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopListEditorComponent } from './components/shop-list-editor/shop-list-editor.component';
import { ShopListRoutingModule } from './shop-list-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EntryPointComponent } from './components/entry-point/entry-point.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    ShopListEditorComponent,
    DashboardComponent,
    EntryPointComponent,
  ],
  imports: [
    CommonModule,
    ShopListRoutingModule,
    MatIconModule,
    MatButtonModule,
  ],
})
export class ShopListModule {}
