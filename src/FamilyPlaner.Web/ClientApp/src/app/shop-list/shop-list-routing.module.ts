import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopListEditorComponent } from './components/shop-list-editor/shop-list-editor.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthorizeGuard } from '../auth';
import { EntryPointComponent } from './components/entry-point/entry-point.component';

const moduleRoutes: Routes = [
  {
    path: 'shoplist', component: EntryPointComponent,
    canActivate: [AuthorizeGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'items', component: ShopListEditorComponent },
      //{ path: 'groceries', component: GroceryCategoryComponent },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(moduleRoutes)],
  exports: [RouterModule],
})
export class ShopListRoutingModule {}
