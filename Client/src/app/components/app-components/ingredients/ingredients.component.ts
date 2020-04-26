import { Component, OnInit } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ApiService } from 'src/app/services/api.service';
import { IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'app-ingredients',
    templateUrl: './ingredients.component.html',
    styleUrls: ['./ingredients.component.scss']
})
export class IngredientsComponent implements OnInit {

    ingredients: IIngredientDto[];

    constructor(
        private controller: ApiService,
        private pageLoader: PageLoaderService) { }

    ngOnInit() {
        this.getIngredients();
    }

    private getIngredients() {
        this.pageLoader.show('Fetching ingredients');
        this.controller.getAllIngredients()
        .subscribe(
            response => {
                this.ingredients = response;
                this.pageLoader.hide();
            },
            error => {
                this.pageLoader.hide();
        });
    }

}
