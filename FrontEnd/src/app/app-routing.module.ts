import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {path: '', component: LayoutComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'admin', component: AdminPanelComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
