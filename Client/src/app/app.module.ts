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
import { ConfirmModalDialog } from './shared/confirm-modal/confirm-modal.component';
import { CocktailsComponent } from './components/app-components/cocktails/cocktails.component';
import { IngredientsComponent } from './components/app-components/ingredients/ingredients.component';
import { EventsComponent } from './components/app-components/events/events.component';
import { IngredientComponent } from './components/app-components/ingredients/ingredient/ingredient.component';
import { EventCardComponent } from './components/app-components/events/event-card/event-card.component';
import { CreateIngredientDialog } from './components/app-components/ingredients/create-ingredient-dialog/create-ingredient.dialog';
import { EditIngredientDialog } from './components/app-components/ingredients/edit-ingredient-dialog/edit-ingredient.dialog';

@NgModule({
  declarations: [
    // Global Components
    AppComponent,
    HeaderComponent,
    FooterComponent,
    IndexComponent,
    PageLoaderComponent,

    // Dialogs
    ConfirmModalDialog,
    
    // Components
    CocktailsComponent,
    IngredientsComponent,
    EventsComponent,
    EventCardComponent,
    IngredientComponent,

    // Dialogues
    CreateIngredientDialog,
    EditIngredientDialog
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    ApiService,
    PageLoaderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
