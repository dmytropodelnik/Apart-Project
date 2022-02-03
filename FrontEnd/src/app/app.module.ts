import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './layout/layout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { AuthComponent } from './auth/auth.component';
import { AdminPanelComponent } from './admin-panel/admin-main-body/admin-panel.component';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthorizationService } from './services/authorization.service';
import { AdminContentComponent } from './admin-panel/admin-content/admin-content.component';
import { UserListComponent } from './admin-panel/lists/users-list/user-list.component';
import { UserProfileListComponent } from './admin-panel/lists/user-profiles-list/user-profile-list.component';
import { CountriesListComponent } from './admin-panel/lists/countries-list/countries-list.component';
import { CitiesListComponent } from './admin-panel/lists/cities-list/cities-list.component';
import { RegionsListComponent } from './regions-list/regions-list.component';
import { DistrictsListComponent } from './admin-panel/lists/districts-list/districts-list.component';
import { AdminAuthComponent } from './admin-auth/admin-auth.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { VerifyEnterComponent } from './verify-enter/verify-enter.component';
import { FlightsComponent } from './flights/flights.component';
import { JoinAsPartnerComponent } from './join-as-partner/join-as-partner.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    LayoutComponent,
    AuthComponent,
    AdminPanelComponent,
    AdminContentComponent,
    UserListComponent,
    UserProfileListComponent,
    CountriesListComponent,
    CitiesListComponent,
    RegionsListComponent,
    DistrictsListComponent,
    AdminAuthComponent,
    FileUploadComponent,
    VerifyEnterComponent,
    FlightsComponent,
    JoinAsPartnerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    SlickCarouselModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AuthorizationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
