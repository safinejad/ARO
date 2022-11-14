import {
    AfterContentInit,
    AfterViewInit,
    Component,
    ContentChildren,
    ElementRef, EventEmitter,
    Input,
    OnChanges,
    OnInit, Output,
    QueryList,
    SimpleChanges,
    ViewChild
} from '@angular/core';
import {MapMarkerDirective} from "./map-marker/map-marker.directive";

declare var L: any;

@Component({
    selector: 'al-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnChanges, AfterContentInit {
    /// Properties
    private _isReady: boolean = false;


    /// Decorators
    @Input() center: number[];
    @Output() public selectMarker = new EventEmitter();
    @ViewChild('mapContainer') private _mapContainer: ElementRef<HTMLDivElement>;
    @ContentChildren(MapMarkerDirective) private _markers: QueryList<MapMarkerDirective>;


    /// Methods
    public ngAfterContentInit(): void {
        setTimeout(() => this._appendScriptsAndStyles(), 200);
    }

    public ngOnChanges(changes: SimpleChanges): void {
        {
            if (this._isReady) {
                this._initMap();
            }
            this._isReady = !this._isReady;
        }
    }

    private _appendScriptsAndStyles(): void {
        const script = document.createElement('script');
        const styles = document.createElement('link');

        script.src = 'https://unpkg.com/leaflet@1.6.0/dist/leaflet.js';
        script.async = true;
        styles.href = 'https://unpkg.com/leaflet@1.6.0/dist/leaflet.css';
        styles.rel = 'stylesheet';

        document.head.appendChild(styles);
        document.head.appendChild(script);

        script.addEventListener('load', () => {
            if (this._isReady) {
                this._initMap();
            }
            this._isReady = !this._isReady;
        }, false);
    }

    private _initMap(): void {
        const map = L.map(this._mapContainer.nativeElement).setView(this.center, 8);

        // Set up the OSM layer
        L.tileLayer('https://maps.alibaba.ir/osm-intl/{z}/{x}/{y}.png', {
            maxZoom: 18
        }).addTo(map);

         this._markers.forEach(item => {
                 const a = L.marker(item.marker)
                .addTo(map)
                .bindTooltip(item.label, {
                    offset: L.point(80, 0),

                })
                 .on('click', (event: any) => {
                     this.selectMarker.emit({
                         label: event.target.getTooltip().getContent(),
                         latlng: event.latlng
                     })
                 });

        })
    }
}
