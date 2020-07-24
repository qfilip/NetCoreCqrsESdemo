import { Injectable } from '@angular/core';
import * as g from '../_notgenerated/globals';
import { HttpClient } from '@angular/common/http';
import { IBaseDto, ICommandInfo } from '../_generated/interfaces';
import { Observable } from 'rxjs';
import { eControllerType } from '../_notgenerated/enums';
import { map } from 'rxjs/operators';

@Injectable()
export class ApiService {
    constructor(private http: HttpClient) {}
    
    private getControllerUrl(controller: eControllerType) {
        let url = '';
        if(controller === eControllerType.Cocktail) url += g.cocktailController;
        else if(controller === eControllerType.Ingredient) url += g.ingredientController;
        else if(controller === eControllerType.Event) url += g.eventController;

        return url;
    }

    // api
    reseedDb() {
        const url = g.cocktailController + 'seed';
        return this.http.get(url);
    }
    executeCommands<T extends IBaseDto>(commands: ICommandInfo<T>[], controller: eControllerType): Observable<ICommandInfo<T>[]> {
        const url = this.getControllerUrl(controller) + g.action;
        return this.http.post<ICommandInfo<T>[]>(url, commands);
    }

    getAll<T extends IBaseDto>(controller: eControllerType): Observable<T[]> {
        const url = this.getControllerUrl(controller) + g.all;
        return this.http.get<T[]>(url);
    }

    get<T extends IBaseDto>(controller: eControllerType): Observable<T> {
        const url = this.getControllerUrl(controller) + g.one;
        return this.http.get<T>(url);
    }
    test() {
        console.log('called');
    }
}