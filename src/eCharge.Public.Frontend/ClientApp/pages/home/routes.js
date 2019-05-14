import makeRoute from 'lib/routing/generator';
import layout from 'components/layout';
import view from './view';

const routes = [
    makeRoute('/', layout, view),
];

export default routes;
