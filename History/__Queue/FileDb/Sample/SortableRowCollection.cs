using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Linq;

using FileDbNs;

namespace SampleApp
{
    class SortableRowCollection: ObservableCollection<Record>, ICollectionView
#if !SILVERLIGHT
        , IEditableCollectionView
#endif
    {
#if !SILVERLIGHT
        Record _currentAddItem, _currentEditItem;
#endif

        CustomSortDescriptionCollection _sort;

        bool _suppressCollectionChanged = false;

        object _currentItem;

        CultureInfo _culture;

        int _currentPosition;

        Predicate<object> _filter;

        ObservableCollection<GroupDescription> _groupDescriptions;

        //-----------------------------------------------------------------------------------------
        public SortableRowCollection( Records rows )
        {
            _currentItem = null;
            _currentPosition = -1;

            foreach( Record row in rows )
                this.Add( row );
        }


        protected override void OnCollectionChanged( NotifyCollectionChangedEventArgs e )
        {
            if( _suppressCollectionChanged )
                return;

            base.OnCollectionChanged( e );
        }

        protected override void SetItem( int index, Record item )
        {
            base.SetItem( index, item );
        }

        protected override void InsertItem( int index, Record item )
        {
            base.InsertItem( index, item );

            if( 0 == index || null == _currentItem )
            {
                _currentItem = item;
                _currentPosition = index;
            }
        }

        public virtual object GetItemAt( int index )
        {
            if( (index >= 0) && (index < this.Count) )
            {
                return this[index];
            }

            return null;
        }


        #region ICollectionView Members

        public bool CanFilter
        {
            get { return false; }
        }

        public bool CanGroup
        {
            get { return false; }
        }

        public bool CanSort
        {
            get { return true; }
        }

        public bool Contains( object item )
        {
            if( !IsValidType( item ) )
            {
                return false;
            }

            return base.Contains( (Record) item );
        }

        private bool IsValidType( object item )
        {
            return item is Record;
        }

        public System.Globalization.CultureInfo Culture
        {
            get
            {
                return this._culture;
            }
            set
            {
                if( this._culture != value )
                {
                    this._culture = value;
                    this.OnPropertyChanged( new PropertyChangedEventArgs( "Culture" ) );
                }
            }
        }

        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;

        public object CurrentItem
        {
            get { return this._currentItem; }
        }

        public int CurrentPosition
        {
            get { return this._currentPosition; }
        }

        public IDisposable DeferRefresh()
        {
            return new SortableCollectionDeferRefresh( this );
        }

        public Predicate<object> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get
            {
                //if( _groupDescriptions == null )
                //    _groupDescriptions = new ObservableCollection<GroupDescription>();
                return _groupDescriptions;
            }
        }

        public ReadOnlyObservableCollection<object> Groups
        {
            get
            {
                return new ReadOnlyObservableCollection<object>( new ObservableCollection<object>() );
            }

        }

        public bool IsCurrentAfterLast
        {
            get
            {
                if( !this.IsEmpty )
                {
                    return (this.CurrentPosition >= this.Count);
                }
                return true;
            }
        }

        public bool IsCurrentBeforeFirst
        {
            get
            {
                if( !this.IsEmpty )
                {
                    return (this.CurrentPosition < 0);
                }
                return true;
            }
        }

        protected bool IsCurrentInSync
        {
            get
            {
                if( this.IsCurrentInView )
                {
                    return (this.GetItemAt( this.CurrentPosition ) == this.CurrentItem);
                }

                return (this.CurrentItem == null);
            }
        }

        private bool IsCurrentInView
        {
            get
            {
                return ((0 <= this.CurrentPosition) && (this.CurrentPosition < this.Count));
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.Count == 0);
            }
        }

        public bool MoveCurrentTo( object item )
        {
            if( !IsValidType( item ) )
            {
                return false;
            }

            if( object.Equals( this.CurrentItem, item ) && ((item != null) || this.IsCurrentInView) )
            {
                return this.IsCurrentInView;
            }

            int index = this.IndexOf( (Record) item );

            return this.MoveCurrentToPosition( index );
        }

        public bool MoveCurrentToFirst()
        {
            return this.MoveCurrentToPosition( 0 );
        }

        public bool MoveCurrentToLast()
        {
            return this.MoveCurrentToPosition( this.Count - 1 );
        }

        public bool MoveCurrentToNext()
        {
            return ((this.CurrentPosition < this.Count) && this.MoveCurrentToPosition( this.CurrentPosition + 1 ));
        }

        public bool MoveCurrentToPrevious()
        {
            return ((this.CurrentPosition >= 0) && this.MoveCurrentToPosition( this.CurrentPosition - 1 ));
        }

        public bool MoveCurrentToPosition( int position )
        {
            if( (position < -1) || (position > this.Count) )
            {
                throw new ArgumentOutOfRangeException( "position" );
            }

            if( ((position != this.CurrentPosition) || !this.IsCurrentInSync) && this.OKToChangeCurrent() )
            {
                bool isCurrentAfterLast = this.IsCurrentAfterLast;
                bool isCurrentBeforeFirst = this.IsCurrentBeforeFirst;

                ChangeCurrentToPosition( position );

                OnCurrentChanged();

                if( this.IsCurrentAfterLast != isCurrentAfterLast )
                {
                    this.OnPropertyChanged( "IsCurrentAfterLast" );
                }

                if( this.IsCurrentBeforeFirst != isCurrentBeforeFirst )
                {
                    this.OnPropertyChanged( "IsCurrentBeforeFirst" );
                }

                this.OnPropertyChanged( "CurrentPosition" );
                this.OnPropertyChanged( "CurrentItem" );
            }

            return this.IsCurrentInView;
        }

        private void ChangeCurrentToPosition( int position )
        {
            if( position < 0 )
            {
                this._currentItem = null;
                this._currentPosition = -1;
            }
            else if( position >= this.Count )
            {
                this._currentItem = null;
                this._currentPosition = this.Count;
            }
            else
            {
                this._currentItem = this[position];
                this._currentPosition = position;
            }
        }

        protected bool OKToChangeCurrent()
        {
            CurrentChangingEventArgs args = new CurrentChangingEventArgs();
            this.OnCurrentChanging( args );
            return !args.Cancel;
        }

        protected virtual void OnCurrentChanged()
        {
            if( this.CurrentChanged != null )
            {
                this.CurrentChanged( this, EventArgs.Empty );
            }
        }

        protected virtual void OnCurrentChanging( CurrentChangingEventArgs args )
        {
            if( args == null )
            {
                throw new ArgumentNullException( "args" );
            }

            if( this.CurrentChanging != null )
            {
                this.CurrentChanging( this, args );
            }
        }

        protected void OnCurrentChanging()
        {
            this._currentPosition = -1;
            this.OnCurrentChanging( new CurrentChangingEventArgs( false ) );
        }

        protected override void ClearItems()
        {
            OnCurrentChanging();
            base.ClearItems();
        }

        private void OnPropertyChanged( string propertyName )
        {
            this.OnPropertyChanged( new PropertyChangedEventArgs( propertyName ) );
        }

        public void Refresh()
        {
            IEnumerable<Record> rows = this;
            IOrderedEnumerable<Record> orderedRecords = null;

            // use the OrderBy and ThenBy LINQ extension methods to sort our data
            bool firstSort = true;
            for( int sortIndex = 0; sortIndex < _sort.Count; sortIndex++ )
            {
                SortDescription sort = _sort[sortIndex];
                Func<Record, object> function = row => row[sort.PropertyName];
                if( firstSort )
                {
                    orderedRecords = sort.Direction == ListSortDirection.Ascending ?
                        rows.OrderBy( function ) : rows.OrderByDescending( function );

                    firstSort = false;
                }
                else
                {
                    orderedRecords = sort.Direction == ListSortDirection.Ascending ?
                        orderedRecords.ThenBy( function ) : orderedRecords.ThenByDescending( function );
                }
            }

            _suppressCollectionChanged = true;

            // re-order this collection based on the result if there is any ordering
            if( orderedRecords != null )
            {
                int index = 0;
                foreach( var row in orderedRecords )
                {
                    this[index++] = row;
                }
            }
            
            _suppressCollectionChanged = false;

            // raise the required notification
            this.OnCollectionChanged( new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        public SortDescriptionCollection SortDescriptions
        {
            get
            {
                if( this._sort == null )
                {
                    this.SetSortDescriptions( new CustomSortDescriptionCollection() );
                }
                return this._sort;
            }
        }

        private void SetSortDescriptions( CustomSortDescriptionCollection descriptions )
        {
            if( this._sort != null )
            {
                this._sort.MyCollectionChanged -= new NotifyCollectionChangedEventHandler( this.SortDescriptionsChanged );
            }

            this._sort = descriptions;

            if( this._sort != null )
            {
                this._sort.MyCollectionChanged += new NotifyCollectionChangedEventHandler( this.SortDescriptionsChanged );
            }
        }

        private void SortDescriptionsChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( e.Action == NotifyCollectionChangedAction.Remove && e.NewStartingIndex == -1 && SortDescriptions.Count > 0 )
            {
                return;
            }
            if( ((e.Action != NotifyCollectionChangedAction.Reset) || (e.NewItems != null))
                || (((e.NewStartingIndex != -1) || (e.OldItems != null)) || (e.OldStartingIndex != -1)) )
            {
                this.Refresh();
            }
        }

        public System.Collections.IEnumerable SourceCollection
        {
            get
            {
                return this;
            }
        }

        #endregion


#if !SILVERLIGHT
        #region Implementation of IEditableCollectionView

        /// <summary>
        /// Adds a new item to the underlying collection.
        /// </summary>
        /// <returns>
        /// The new item that is added to the collection.
        /// </returns>
        public object AddNew()
        {
            CommitNew();
            CommitEdit();

            /* we're not going to allow adding just now
            _currentAddItem = new Record();
            this.Add( _currentAddItem );

            //_currentAddItem.BeginEdit();
            MoveCurrentToFirst();
            */
            return _currentAddItem;
        }

        /// <summary>
        /// Ends the add transaction and saves the pending new item.
        /// </summary>
        public void CommitNew()
        {
            if (IsAddingNew)
            {
                //_currentAddItem.EndEdit();
                _currentAddItem = null;
            }
        }

        /// <summary>
        /// Ends the add transaction and discards the pending new item.
        /// </summary>
        public void CancelNew()
        {
            if( IsAddingNew )
            {
                //_currentAddItem.CancelEdit();
                MoveCurrentToLast();

                Remove( _currentAddItem );
                _currentAddItem = null;
            }
        }

        /// <summary>
        /// Removes the item at the specified position from the collection.
        /// </summary>
        /// <param name="index">Index of item to remove.</param>
        public new void RemoveAt( int index )
        {
            if( index < 0 || index >= this.Count )
            {
                throw new IndexOutOfRangeException( "index must be at least 0 and less than the Count" );
            }

            Remove( this.GetItemAt( index ) );
        }

        /// <summary>
        /// Removes the specified item from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove( object item )
        {
            var entity = item as Record;

            if( entity != null )
            {
                this.Remove( entity );

                if( IsEmpty )
                {
                    _currentAddItem = null;
                    /*
                    CurrentItem = null;
                    CurrentPosition = -1;
                    RaiseCurrentChangingEvent( new CurrentChangingEventArgs( false ) );
                    RaisePropertyChanged( () => CurrentItem );
                    RaisePropertyChanged( () => CurrentPosition );
                    RaisePropertyChanged( () => IsCurrentBeforeFirst );
                    RaiseCurrentChangedEvent( EventArgs.Empty );
                    */
                }
                else
                {
                    MoveCurrentTo( CurrentPosition );
                }
            }
        }

        /// <summary>
        /// Begins an edit transaction on the specified item.
        /// </summary>
        /// <param name="item">The item to edit.</param>
        public void EditItem( object item )
        {
            CommitNew();
            CommitEdit();

            _currentEditItem = item as Record;
            MoveCurrentTo( item );

            if( _currentEditItem != null )
            {
                //_currentEditItem.BeginEdit();
            }
        }

        /// <summary>
        /// Ends the edit transaction and saves the pending changes.
        /// </summary>
        public void CommitEdit()
        {
            if( IsEditingItem )
            {
                //_currentEditItem.EndEdit();
                _currentEditItem = null;
            }
        }

        /// <summary>
        /// Ends the edit transaction and, if possible, restores the original value of the item.
        /// </summary>
        public void CancelEdit()
        {
            if( IsEditingItem )
            {
                //_currentEditItem.CancelEdit();
            }
        }

        /// <summary>
        /// Gets or sets the position of the new item placeholder in the collection view.
        /// </summary>
        /// <returns>
        /// An enumeration value that specifies the position of the new item placeholder in the collection view.
        /// </returns>
        public NewItemPlaceholderPosition NewItemPlaceholderPosition
        {
            get { return (NewItemPlaceholderPosition.None); }
            set { /*throw new NotSupportedException();*/ }
        }

        /// <summary>
        /// Gets a value that indicates whether a new item can be added to the collection.
        /// </summary>
        /// <returns>
        /// true if a new item can be added to the collection; otherwise, false.
        /// </returns>
        public bool CanAddNew
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value that indicates whether an add transaction is in progress.
        /// </summary>
        /// <returns>
        /// true if an add transaction is in progress; otherwise, false.
        /// </returns>
        public bool IsAddingNew
        {
            get { return _currentAddItem != null; }
        }

        /// <summary>
        /// Gets the item that is being added during the current add transaction.
        /// </summary>
        /// <returns>
        /// The item that is being added if <see cref="P:System.ComponentModel.IEditableCollectionView.IsAddingNew"/> is true; otherwise, null.
        /// </returns>
        public object CurrentAddItem
        {
            get { return _currentAddItem; }
        }

        /// <summary>
        /// Gets a value that indicates whether an item can be removed from the collection.
        /// </summary>
        /// <returns>
        /// true if an item can be removed from the collection; otherwise, false.
        /// </returns>
        public bool CanRemove
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value that indicates whether the collection view can discard pending changes and restore the original values of an edited object.
        /// </summary>
        /// <returns>
        /// true if the collection view can discard pending changes and restore the original values of an edited object; otherwise, false.
        /// </returns>
        public bool CanCancelEdit
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value that indicates whether an edit transaction is in progress.
        /// </summary>
        /// <returns>
        /// true if an edit transaction is in progress; otherwise, false.
        /// </returns>
        public bool IsEditingItem
        {
            get { return _currentEditItem != null; }
        }

        /// <summary>
        /// Gets the item in the collection that is being edited.
        /// </summary>
        /// <returns>
        /// The item that is being edited if <see cref="P:System.ComponentModel.IEditableCollectionView.IsEditingItem"/> is true; otherwise, null.
        /// </returns>
        public object CurrentEditItem
        {
            get { return _currentEditItem; }
        }
        #endregion
#endif
    }

    public class SortableCollectionDeferRefresh : IDisposable
    {
        private readonly SortableRowCollection _collectionView;

        internal SortableCollectionDeferRefresh( SortableRowCollection collectionView )
        {
            _collectionView = collectionView;
        }

        public void Dispose()
        {
            // refresh the collection when disposed.
            _collectionView.Refresh();
        }
    }


    public class CustomSortDescriptionCollection : SortDescriptionCollection
    {

        public event NotifyCollectionChangedEventHandler MyCollectionChanged
        {
            add
            {
                this.CollectionChanged += value;
            }
            remove
            {
                this.CollectionChanged -= value;
            }
        }
    }
}
