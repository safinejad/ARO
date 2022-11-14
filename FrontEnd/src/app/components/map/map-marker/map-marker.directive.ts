import {Directive, Input} from '@angular/core';

@Directive({
    selector: 'al-map-marker'
})
export class MapMarkerDirective {
    /// Decorators
    @Input() public marker: number[];
    @Input() public label: string;



}
