import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from './auth';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HomeComponent } from './pages/home/home.component';
import { NotFoundComponent } from '@shared-module';

const appRoutes: Routes = [
  {path:'home', component: HomeComponent},
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthorizeGuard],
  },
  { path: '',   redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
