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
import { UserProfilesListComponent } from './admin-panel/lists/user-profiles-list/user-profiles-list.component';
import { CountriesListComponent } from './admin-panel/lists/countries-list/countries-list.component';
import { CitiesListComponent } from './admin-panel/lists/cities-list/cities-list.component';
import { DistrictsListComponent } from './admin-panel/lists/districts-list/districts-list.component';
import { AdminAuthComponent } from './admin-auth/admin-auth.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { VerifyEnterComponent } from './verify-enter/verify-enter.component';
import { FlightsComponent } from './flights/flights.component';
import { JoinAsPartnerComponent } from './join-as-partner/join-as-partner.component';
import { RolesListComponent } from './admin-panel/lists/roles-list/roles-list.component';
import { GendersListComponent } from './admin-panel/lists/genders-list/genders-list.component';
import { RoomTypesListComponent } from './admin-panel/lists/room-types-list/room-types-list.component';
import { RoomsListComponent } from './admin-panel/lists/rooms-list/rooms-list.component';
import { SuggestionsListComponent } from './admin-panel/lists/suggestions-list/suggestions-list.component';
import { SuggestionHighlightsListComponent } from './admin-panel/lists/suggestion-highlights-list/suggestion-highlights-list.component';
import { SuggestionRulesListComponent } from './admin-panel/lists/suggestion-rules-list/suggestion-rules-list.component';
import { SuggestionRuleTypesListComponent } from './admin-panel/lists/suggestion-rule-types-list/suggestion-rule-types-list.component';
import { SuggestionReviewGradesListComponent } from './admin-panel/lists/suggestion-review-grades-list/suggestion-review-grades-list.component';
import { SurroundingObjectsListComponent } from './admin-panel/lists/surrounding-objects-list/surrounding-objects-list.component';
import { SurroundingObjectTypesListComponent } from './admin-panel/lists/surrounding-object-types-list/surrounding-object-types-list.component';
import { FavoritesListComponent } from './admin-panel/lists/favorites-list/favorites-list.component';
import { TempUsersListComponent } from './admin-panel/lists/temp-users-list/temp-users-list.component';
import { FacilitiesListComponent } from './admin-panel/lists/facilities-list/facilities-list.component';
import { FacilityTypesListComponent } from './admin-panel/lists/facility-types-list/facility-types-list.component';
import { FileModelsListComponent } from './admin-panel/lists/file-models-list/file-models-list.component';
import { LanguagesListComponent } from './admin-panel/lists/languages-list/languages-list.component';
import { NotificationsListComponent } from './admin-panel/lists/notifications-list/notifications-list.component';
import { ServiceCategoriesListComponent } from './admin-panel/lists/service-categories-list/service-categories-list.component';
import { PaymentsListComponent } from './admin-panel/lists/payments-list/payments-list.component';
import { CardTypesListComponent } from './admin-panel/lists/card-types-list/card-types-list.component';
import { CreditCardsListComponent } from './admin-panel/lists/credit-cards-list/credit-cards-list.component';
import { CurrenciesListComponent } from './admin-panel/lists/currencies-list/currencies-list.component';
import { PromoCodesListComponent } from './admin-panel/lists/promo-codes-list/promo-codes-list.component';
import { PaymentTypesListComponent } from './admin-panel/lists/payment-types-list/payment-types-list.component';
import { RegionsListComponent } from './admin-panel/lists/regions-list/regions-list.component';
import { AirportsListComponent } from './admin-panel/lists/airports-list/airports-list.component';
import { AddressesListComponent } from './admin-panel/lists/addresses-list/addresses-list.component';
import { BookingPricesListComponent } from './admin-panel/lists/booking-prices-list/booking-prices-list.component';
import { FlightClassTypesListComponent } from './admin-panel/lists/flight-class-types-list/flight-class-types-list.component';
import { StayBookingsListComponent } from './admin-panel/lists/stay-bookings-list/stay-bookings-list.component';
import { FlightBookingsListComponent } from './admin-panel/lists/flight-bookings-list/flight-bookings-list.component';
import { CarRentalBookingsListComponent } from './admin-panel/lists/car-rental-bookings-list/car-rental-bookings-list.component';
import { AttractionBookingsListComponent } from './admin-panel/lists/attraction-bookings-list/attraction-bookings-list.component';
import { AirportTaxiBookingsListComponent } from './admin-panel/lists/airport-taxi-bookings-list/airport-taxi-bookings-list.component';
import { ReviewsListComponent } from './admin-panel/lists/reviews-list/reviews-list.component';
import { ReviewCategoriesListComponent } from './admin-panel/lists/review-categories-list/review-categories-list.component';
import { ReviewMessagesListComponent } from './admin-panel/lists/review-messages-list/review-messages-list.component';
import { BookingCategoriesListComponent } from './admin-panel/lists/booking-categories-list/booking-categories-list.component';
import { ViewPropertyComponent } from './view-property/view-property.component';
import { AddPropertyComponent } from './add-property/add-property.component';
import { MainDataService } from './services/main-data.service';

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
    CountriesListComponent,
    CitiesListComponent,
    DistrictsListComponent,
    AdminAuthComponent,
    FileUploadComponent,
    VerifyEnterComponent,
    FlightsComponent,
    JoinAsPartnerComponent,
    RolesListComponent,
    GendersListComponent,
    RoomTypesListComponent,
    RoomsListComponent,
    SuggestionsListComponent,
    SuggestionHighlightsListComponent,
    SuggestionRulesListComponent,
    SuggestionRuleTypesListComponent,
    SuggestionReviewGradesListComponent,
    SurroundingObjectsListComponent,
    SurroundingObjectTypesListComponent,
    FavoritesListComponent,
    TempUsersListComponent,
    FacilitiesListComponent,
    FacilityTypesListComponent,
    FileModelsListComponent,
    LanguagesListComponent,
    NotificationsListComponent,
    ServiceCategoriesListComponent,
    PaymentsListComponent,
    CardTypesListComponent,
    CreditCardsListComponent,
    CurrenciesListComponent,
    PromoCodesListComponent,
    PaymentTypesListComponent,
    RegionsListComponent,
    AirportsListComponent,
    AddressesListComponent,
    BookingPricesListComponent,
    FlightClassTypesListComponent,
    UserProfilesListComponent,
    StayBookingsListComponent,
    FlightBookingsListComponent,
    CarRentalBookingsListComponent,
    AttractionBookingsListComponent,
    AirportTaxiBookingsListComponent,
    ReviewsListComponent,
    ReviewCategoriesListComponent,
    ReviewMessagesListComponent,
    ViewPropertyComponent,
    AddPropertyComponent,
    BookingCategoriesListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    SlickCarouselModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AuthorizationService, MainDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
