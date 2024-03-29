import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AdminPanelComponent } from './admin-panel/admin-main-body/admin-panel.component';
import { LayoutComponent } from './layout/layout.component';
import { AdminAuthComponent } from './admin-auth/admin-auth.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { VerifyEnterComponent } from './verify-enter/verify-enter.component';
import { AdminPanelGuard } from './admin-panel/admin-main-body/admin-panel.guard';
import { JoinAsPartnerComponent } from './join-as-partner/join-as-partner.component';
import { ViewPropertyComponent as ViewPropertiesComponent } from './view-properties/view-properties.component';
import { AddPropertyComponent } from './list-your-property/add-property/add-property.component';
import { LpNameAndLocationComponent } from './list-your-property/lp-name-and-location/lp-name-and-location.component';
import { LpPhotosComponent } from './list-your-property/lp-photos/lp-photos.component';
import { LpPricingAndCalenderComponent } from './list-your-property/lp-pricing-and-calender/lp-pricing-and-calender.component';
import { LpPropertySetupComponent } from './list-your-property/lp-property-setup/lp-property-setup.component';
import { LpReviewAndCompleteComponent } from './list-your-property/lp-review-and-complete/lp-review-and-complete.component';
import { ManageAccountComponent } from './manage-account/manage-account-body/manage-account.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { UserSavedComponent } from './user-saved/user-saved.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { LpApartmentsComponent } from './list-your-property/lp-apartments/lp-apartments.component';
import { StaySuggestionPageComponent } from './stay-suggestion-page/stay-suggestion-page.component';
import { ManageAccountGuard } from './manage-account/manage-account-body/manage-account.guard';
import { ViewPropertiesGuard } from './view-properties/view-properties.guard';
import { UserBookingsComponent } from './user-bookings/user-bookings.component';
import { FillingUserDetailsComponent } from './booking-properties/filling-user-details/filling-user-details.component';
import { BookingFinalStepComponent } from './booking-properties/booking-final-step/booking-final-step.component';
import { UsersReviewsComponent } from './users-reviews/users-reviews.component';

const routes: Routes = [
  { path: '', redirectTo: 'stays', pathMatch: 'full' },
  { path: 'stays', component: LayoutComponent },
  { path: 'auth', component: AuthComponent },
  {
    path: 'admin',
    component: AdminPanelComponent,
    canActivate: [AdminPanelGuard],
  },
  { path: 'adminlogin', component: AdminAuthComponent },
  { path: 'upload', component: FileUploadComponent },
  { path: 'confirmemail', component: VerifyEnterComponent },
  { path: 'joinpartner', component: JoinAsPartnerComponent },
  { path: 'saved', component: UserSavedComponent },
  {
    path: 'viewproperties',
    component: ViewPropertiesComponent,
    canActivate: [ViewPropertiesGuard],
  },
  {
    path: 'lp/addproperty',
    component: AddPropertyComponent,
    pathMatch: 'full',
  },
  {
    path: 'mysettings',
    component: ManageAccountComponent,
    canActivate: [ManageAccountGuard],
  },
  { path: 'searchresults', component: SearchResultsComponent },
  { path: 'suggestion/:id', component: StaySuggestionPageComponent },
  { path: 'resetpassword', component: ResetPasswordComponent },
  {
    path: 'lp/nameandlocation',
    component: LpNameAndLocationComponent,
    pathMatch: 'full',
  },
  {
    path: 'lp/photos',
    component: LpPhotosComponent,
    pathMatch: 'full',
  },
  {
    path: 'lp/pricingandcalender',
    component: LpPricingAndCalenderComponent,
    pathMatch: 'full',
  },
  {
    path: 'lp/propertysetup',
    component: LpPropertySetupComponent,
    pathMatch: 'full',
  },
  {
    path: 'lp/reviewandcomplete',
    component: LpReviewAndCompleteComponent,
    pathMatch: 'full',
  },
  {
    path: 'lp/apartments',
    component: LpApartmentsComponent,
    pathMatch: 'full',
  },
  { path: 'userbookings', component: UserBookingsComponent, pathMatch: 'full' },
  {
    path: 'fillinguserdetails',
    component: FillingUserDetailsComponent,
    pathMatch: 'full',
  },
  {
    path: 'bookingfinalstep',
    component: BookingFinalStepComponent,
    pathMatch: 'full',
  },
  {
    path: 'usersreviews',
    component: UsersReviewsComponent,
    pathMatch: 'full',
  },
  { path: '**', redirectTo: 'stays', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
  ],
  exports: [RouterModule],
  providers: [AdminPanelGuard, ManageAccountGuard, ViewPropertiesGuard],
})
export class AppRoutingModule {}
