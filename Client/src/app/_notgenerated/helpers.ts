import { eCommand, eCommandType } from '../_generated/enums';
import { IBaseDto } from '../_generated/interfaces';

export class PageLoaderInfo {
    loading: boolean;
    message: string;
}

export class ConfirmDialogInfo {
    visible: boolean;
    message: string;
    okLabel: string;
    cancelLabel: string;
    cancelVisible: boolean;
}

export class Command {
    private array: IBaseDto[];
    
    constructor(parameter: IBaseDto, array: IBaseDto[], command: eCommand, commandType: eCommandType, description: string = 'no description') {
        this.parameter = parameter;
        this.array = array;
        this.command = command;
        this.description = description;
        
        this.execute = () => this.array.push(parameter);
        this.reverse = () => {
            const index = this.array.findIndex(x => x.id === this.parameter.id);
            this.array.splice(index, 1);
        }
    }

    parameter: IBaseDto;
    command: eCommand;
    commandType: eCommandType;
    description: string;
    
    execute: () => any;
    reverse: () => any;
}