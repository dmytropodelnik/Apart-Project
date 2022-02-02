import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AdminPanelComponent } from './admin-panel/admin-main-body/admin-panel.component';
import { LayoutComponent } from './layout/layout.component';
import { AdminAuthComponent } from './admin-auth/admin-auth.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { VerifyEnterComponent } from './verify-enter/verify-enter.component';
import { FlightsComponent } from './flights/flights.component';
import { AdminPanelGuard } from './admin-panel/admin-main-body/admin-panel.guard';

const routes: Routes = [
  {path: '', component: LayoutComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'admin', component: AdminPanelComponent, canActivate: [AdminPanelGuard]},
  {path: 'adminlogin', component: AdminAuthComponent},
  {path: 'upload', component: FileUploadComponent},
  {path: 'confirmemail', component: VerifyEnterComponent},
  {path: 'flights', component: FlightsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AdminPanelGuard],
})
export class AppRoutingModule { }
