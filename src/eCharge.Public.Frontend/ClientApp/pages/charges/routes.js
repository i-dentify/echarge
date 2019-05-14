import makeRoute from 'lib/routing/generator';
import layout from 'components/layout';
import list from './list';
import add from './add';

const routes = [
    makeRoute('/charges', layout, list),
    makeRoute('/charges/:location/:car', layout, list),
    makeRoute('/charges/add/:location/:car', layout, add),
];

export default routes;
