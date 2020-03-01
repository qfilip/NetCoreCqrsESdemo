import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { PresenterComponent } from './components/presenter/presenter.component';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'presenter', component: PresenterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
