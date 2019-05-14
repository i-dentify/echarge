import m from 'mithril';

function view({ state, attrs }) {
    const { columns } = state;
    const { items } = attrs;
    const renderHeader = cols => cols.map(column => m('th[scope=col]', {
        class: (column.header && column.header.class && column.header.class()) || undefined,
    }, (column.header && column.header.content()) || undefined));
    const renderFooter = (cols) => {
        const contentColumns = cols.filter(column => column.footer && column.footer.colspan > 0);

        if (contentColumns.length > 0) {
            return contentColumns.map(column => m(`td[colspan=${column.footer.colspan}]`, {
                class: (column.footer.class && column.footer.class()) || undefined,
            }, (column.footer.content && column.footer.content()) || ''));
        }

        return m(`td[colspan=${cols.length}]`);
    };
    const renderBody = (cols, records) => records.map(item => m('tr', cols
        ? cols.map(column => m('td', {
            class: (column.body.class && column.body.class(item)) || undefined,
        }, column.body.content(item)))
        : undefined));

    return m('table', [
        m('thead',
            m('tr',
                columns()
                    ? renderHeader(columns())
                    : undefined)),
        m('tfoot',
            m('tr',
                columns()
                    ? renderFooter(columns())
                    : undefined)),
        m('tbody',
            items
                ? renderBody(columns(), items)
                : undefined),
    ]);
}

export default view;
