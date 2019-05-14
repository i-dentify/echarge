import m from 'mithril';
import { mapsApiKey } from 'config';

const resolveAddress = address => m.request({
    method: 'GET',
    url: `https://maps.googleapis.com/maps/api/geocode/json?address=${address}&key=${mapsApiKey}`,
})
    .then((response) => {
        if (response.status.toLocaleLowerCase() === 'ok') {
            const data = response.results.pop();
            return {
                address: data.formatted_address,
                latitude: data.geometry.location.lat,
                longitude: data.geometry.location.lng,
            };
        }

        return undefined;
    })
    .catch(() => undefined);

export default resolveAddress;
