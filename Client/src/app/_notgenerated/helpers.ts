import { eCommand } from '../_generated/enums';
import { IBaseDto } from '../_generated/interfaces';

export class PageLoaderInfo {
    loading: boolean;
    message: string;
}

export class Command {
    private array: IBaseDto[];
    
    constructor(parameter: IBaseDto, array: IBaseDto[], type: eCommand) {
        this.parameter = parameter;
        this.array = array;
        this.execute = () => this.array.push(parameter);
        this.reverse = () => {
            const index = this.array.findIndex(x => x.id === this.parameter.id);
            this.array.splice(index, 1);
        }
    }

    parameter: IBaseDto;
    execute: () => any;
    reverse: () => any;
    commandType: eCommand;
}