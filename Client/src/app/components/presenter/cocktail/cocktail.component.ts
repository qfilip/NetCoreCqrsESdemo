import { Component, OnInit, Input } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';

@Component({
  selector: 'app-cocktail',
  templateUrl: './cocktail.component.html',
  styleUrls: ['./cocktail.component.scss']
})
export class CocktailComponent implements OnInit {
  
  @Input() cocktail : ICocktailDto;
  strength = 'cocktailStrengthHere + %';

  constructor() { }

  ngOnInit(): void {
  }

}
