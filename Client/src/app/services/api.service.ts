import { Injectable } from '@angular/core';
import * as g from '../_notgenerated/globals';
import { HttpClient } from '@angular/common/http';
import { ICocktailDto, IAppEventDto, IIngredientDto, ICommandPayload, IBaseDto } from '../_generated/interfaces';
import { Observable } from 'rxjs';
import { eControllerType } from '../_notgenerated/enums';

@Injectable({providedIn: 'root'})
export class ApiService {
    constructor(private http: HttpClient) {}

    private getApiActionUrl(controller: eControllerType) {
        let url = '';
        if(controller === eControllerType.Cocktail) url += g.cocktailController;
        else if(controller === eControllerType.Ingredient) url += g.ingredientController;
        else if(controller === eControllerType.Event) url += g.eventController;

        return url + g.action;
    }

    // api
    executeCommands<T extends IBaseDto>(commands: ICommandPayload<T>[], controller: eControllerType): Observable<ICommandPayload<T>[]> {
        const url = this.getApiActionUrl(controller);
        return this.http.post<ICommandPayload<T>[]>(url, commands);
    }

    // cocktails
    getAllCocktails(): Observable<ICocktailDto[]> {
        const url = g.cocktailController + g.all;
        return this.http.get<ICocktailDto[]>(url);
    }

    createCocktail(cocktailDto: ICocktailDto): Observable<ICocktailDto> {
        const url = g.cocktailController + g.create;
        return this.http.post<ICocktailDto>(url, cocktailDto);
    }

    deleteCocktail(cocktailDto: ICocktailDto): Observable<ICocktailDto> {
        const url = g.cocktailController + g.remove;
        return this.http.post<ICocktailDto>(url, cocktailDto);
    }


    // Ingredients
    getAllIngredients(): Observable<IIngredientDto[]> {
        const url = g.ingredientController + g.all;
        return this.http.get<IIngredientDto[]>(url);
    }

    // createIngredients(commandPayload: ICommandPayload<IIngredientDto>[]): Observable<IIngredientDto[]> {
    //     const url = g.ingredientController + g.create;
    //     return this.http.post<IIngredientDto[]>(url, commandPayload);
    // }
    
    
    //events
    getAllEvents(): Observable<IAppEventDto[]> {
        const url = g.eventController + g.all;
        return this.http.get<IAppEventDto[]>(url);
    }
}