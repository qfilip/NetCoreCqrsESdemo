import { Command } from '../_notgenerated/helpers';

export class CommandHandler {
    private stack: Command[];
    private index: number;
    constructor() {
        this.stack = [];
        this.index = -1;
    }

    execute(command: Command) {
        command.execute();
        this.stack.push(command);
        this.index++;
    }

    reverse() {
        if(this.index < 0)
            return;

        const command = this.stack[this.index];
        command.reverse();
        this.stack.pop();
        this.index--;
    }

    getChanges() {
        return this.stack;
    }
}