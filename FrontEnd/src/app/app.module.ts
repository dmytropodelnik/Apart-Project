import { APP_INITIALIZER, NgModule } from '@angular/core';
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
import { GuestsListComponent } from './admin-panel/lists/guests-list/guests-list.component';
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
import { StayBookingsListComponent } from './admin-panel/lists/stay-bookings-list/stay-bookings-list.component';
import { ReviewsListComponent } from './admin-panel/lists/reviews-list/reviews-list.component';
import { ReviewCategoriesListComponent } from './admin-panel/lists/review-categories-list/review-categories-list.component';
import { ReviewMessagesListComponent } from './admin-panel/lists/review-messages-list/review-messages-list.component';
import { BookingCategoriesListComponent } from './admin-panel/lists/booking-categories-list/booking-categories-list.component';
import { ViewPropertyComponent } from './view-properties/view-properties.component';
import { AddPropertyComponent } from './list-your-property/add-property/add-property.component';
import { LpNameAndLocationComponent } from './list-your-property/lp-name-and-location/lp-name-and-location.component';
import { LpPropertySetupComponent } from './list-your-property/lp-property-setup/lp-property-setup.component';
import { MainDataService } from './services/main-data.service';
import { LpPhotosComponent } from './list-your-property/lp-photos/lp-photos.component';
import { LpPricingAndCalenderComponent } from './list-your-property/lp-pricing-and-calender/lp-pricing-and-calender.component';
import { LpReviewAndCompleteComponent } from './list-your-property/lp-review-and-complete/lp-review-and-complete.component';
import { ListNewPropertyService } from './services/list-new-property.service';
import { PromocodeGeneratorComponent } from './admin-panel/promocode-generator/promocode-generator.component';
import { ManageAccountComponent } from './manage-account/manage-account-body/manage-account.component';
import { ManageAccountContentComponent } from './manage-account/manage-account-content/manage-account-content.component';
import { PersonalDetailsListComponent } from './manage-account/manage-account-content-list/personal-details-list/personal-details-list.component';
import { PreferencesListComponent } from './manage-account/manage-account-content-list/preferences-list/preferences-list.component';
import { SecurityListComponent } from './manage-account/manage-account-content-list/security-list/security-list.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { AdminContentService } from './services/admin-content.service';
import { UserSavedComponent } from './user-saved/user-saved.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { LpApartmentsComponent } from './list-your-property/lp-apartments/lp-apartments.component';
import { StaySuggestionPageComponent } from './stay-suggestion-page/stay-suggestion-page.component';
import { Router } from '@angular/router';

import {
  SocialLoginModule,
  SocialAuthServiceConfig,
} from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { LetterCreatorComponent } from './admin-panel/letter-creator/letter-creator.component';
import { UserBookingsComponent } from './user-bookings/user-bookings.component';
import { FillingUserDetailsComponent } from './booking-properties/filling-user-details/filling-user-details.component';
import { BookingFinalStepComponent } from './booking-properties/booking-final-step/booking-final-step.component';
import { BookingDetailsService } from './services/booking-details.service';
import { UsersReviewsComponent } from './users-reviews/users-reviews.component';
import { LoaderComponent } from './loader/loader.component';
import { AlertComponent } from './alert/alert.component';

@NgModule({
  providers: [
    AuthorizationService,
    MainDataService,
    ListNewPropertyService,
    AdminContentService,
    BookingDetailsService,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '1051280403604-gn2mjml14fgen0739ts5su7n22vclbiv.apps.googleusercontent.com'
            ),
          },
        ],
      } as SocialAuthServiceConfig,
    },
  ],
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
    GuestsListComponent,
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
    UserProfilesListComponent,
    StayBookingsListComponent,
    ReviewsListComponent,
    ReviewCategoriesListComponent,
    ReviewMessagesListComponent,
    ViewPropertyComponent,
    AddPropertyComponent,
    BookingCategoriesListComponent,
    LpNameAndLocationComponent,
    LpPropertySetupComponent,
    LpPhotosComponent,
    LpPricingAndCalenderComponent,
    LpReviewAndCompleteComponent,
    ManageAccountComponent,
    ManageAccountContentComponent,
    PersonalDetailsListComponent,
    PreferencesListComponent,
    SecurityListComponent,
    SearchResultsComponent,
    PromocodeGeneratorComponent,
    UserSavedComponent,
    ResetPasswordComponent,
    LpApartmentsComponent,
    StaySuggestionPageComponent,
    LetterCreatorComponent,
    UserBookingsComponent,
    FillingUserDetailsComponent,
    BookingFinalStepComponent,
    UsersReviewsComponent,
    LoaderComponent,
    AlertComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    SlickCarouselModule,
    FormsModule,
    ReactiveFormsModule,
    SocialLoginModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

// {
//   provide: APP_INITIALIZER,
//   useFactory: () => appInitializer,
//   deps: [AuthorizationService, Router],
//   multi: true
//  },
