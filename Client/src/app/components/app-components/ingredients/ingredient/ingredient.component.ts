import { Component, OnInit, Input } from '@angular/core';
import { IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'app-ingredient',
    templateUrl: './ingredient.component.html',
    styleUrls: ['./ingredient.component.scss']
})
export class IngredientComponent implements OnInit {

    @Input('ingredient') ingredient: IIngredientDto;

    constructor() { }

    ngOnInit(): void {
    }

}
