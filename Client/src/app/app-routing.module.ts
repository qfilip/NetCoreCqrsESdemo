import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { CocktailsComponent } from './components/app-components/cocktails/cocktails.component';
import { IngredientsComponent } from './components/app-components/ingredients/ingredients.component';
import { EventsComponent } from './components/app-components/events/events.component';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'cocktails', component: CocktailsComponent },
  { path: 'ingredients', component: IngredientsComponent },
  { path: 'events', component: EventsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
