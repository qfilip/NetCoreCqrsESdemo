import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./components/header/header.component";
import { FooterComponent } from "./components/footer/footer.component";
import { IndexComponent } from "./components/index/index.component";
import { PresenterComponent } from './components/presenter/presenter.component';
import { ApiService } from './services/api.service';
import { CocktailComponent } from './components/presenter/cocktail/cocktail.component';
import { PageLoaderComponent } from './shared/page-loader/page-loader.component';
import { PageLoaderService } from './services/page-loader.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    IndexComponent,
    PresenterComponent,
    CocktailComponent,
    PageLoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ApiService,
    PageLoaderService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
