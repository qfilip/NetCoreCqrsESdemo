import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';

@Component({
  selector: 'app-cocktail',
  templateUrl: './cocktail.component.html',
  styleUrls: ['./cocktail.component.scss']
})
export class CocktailComponent {

    @Output('onEditClicked') onEditClicked = new EventEmitter<ICocktailDto>();
    @Output('onRemoveClicked') onRemoveClicked = new EventEmitter<string>();
    
    @Input('cocktail') cocktail: ICocktailDto;

    openEditDialog() {
        this.onEditClicked.emit(this.cocktail);
    }

    openRemoveDialog() {
        this.onRemoveClicked.emit(this.cocktail.id);
    }

}
