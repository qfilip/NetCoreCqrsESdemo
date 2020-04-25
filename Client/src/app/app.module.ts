import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./components/header/header.component";
import { FooterComponent } from "./components/footer/footer.component";
import { IndexComponent } from "./components/index/index.component";
import { ApiService } from './services/api.service';
import { PageLoaderComponent } from './shared/page-loader/page-loader.component';
import { PageLoaderService } from './services/page-loader.service';
import { FormsModule } from '@angular/forms';
import { ConfirmModalComponent } from './shared/confirm-modal/confirm-modal.component';
import { NavPanelComponent } from './components/nav-panel/nav-panel.component';
import { CocktailsComponent } from './components/app-components/cocktails/cocktails.component';
import { IngredientsComponent } from './components/app-components/ingredients/ingredients.component';
import { EventsComponent } from './components/app-components/events/events.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    IndexComponent,
    PageLoaderComponent,
    ConfirmModalComponent,
    NavPanelComponent,
    
    CocktailsComponent,
    IngredientsComponent,
    EventsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    ApiService,
    PageLoaderService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
