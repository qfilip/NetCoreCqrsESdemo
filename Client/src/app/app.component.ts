import { Component } from '@angular/core';
import { IIngredientDto } from './_generated/interfaces';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public currentRoute: string = '';

  public updateCurrentRoute(e: string) {
      this.currentRoute = (e === '') ? '' : `${e} Page`;
  }
}